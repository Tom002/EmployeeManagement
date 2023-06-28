import { Component, Inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { DepartmentClient, DepartmentDto } from 'src/app/api/app.generated';

@Component({
  selector: 'app-department-edit',
  templateUrl: './department-edit.component.html',
  styleUrls: ['./department-edit.component.css'],
})
export class DepartmentEditComponent {
  editDepartmentForm!: FormGroup;

  constructor(
    private readonly departmentClient: DepartmentClient,
    private readonly dialogRef: MatDialogRef<DepartmentEditComponent>,
    @Inject(MAT_DIALOG_DATA) public data: DepartmentDto,
    private readonly fb: FormBuilder
  ) {
    this.initForm();
  }

  initForm() {
    this.editDepartmentForm = this.fb.group({
      name: [this.data.name, [Validators.required, Validators.maxLength(100)]],
      abbreviation: [
        this.data.abbreviation,
        [Validators.required, Validators.maxLength(5)],
      ],
    });
  }

  cancel() {
    this.dialogRef.close();
  }

  submitForm() {
    this.departmentClient
      .departmentPUT(this.data.id!, {
        name: this.editDepartmentForm.controls['name'].value,
        abbreviation: this.editDepartmentForm.controls['abbreviation'].value,
      })
      .subscribe(() => {
        this.dialogRef.close(true);
      });
  }

  public errorHandling = (control: string, error: string) => {
    return this.editDepartmentForm.controls[control].hasError(error);
  };
}
