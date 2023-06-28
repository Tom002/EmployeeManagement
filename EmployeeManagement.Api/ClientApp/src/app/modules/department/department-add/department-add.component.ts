import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import {
  DepartmentClient,
  DepartmentCreateOrUpdateDto,
} from 'src/app/api/app.generated';

@Component({
  selector: 'app-department-add',
  templateUrl: './department-add.component.html',
  styleUrls: ['./department-add.component.css'],
})
export class DepartmentAddComponent {
  addDepartmentForm!: FormGroup;

  constructor(
    private readonly departmentClient: DepartmentClient,
    private readonly dialogRef: MatDialogRef<DepartmentAddComponent>,
    private readonly fb: FormBuilder
  ) {
    this.initForm();
  }

  initForm() {
    this.addDepartmentForm = this.fb.group({
      name: ['', [Validators.required, Validators.maxLength(100)]],
      abbreviation: ['', [Validators.required, Validators.maxLength(5)]],
    });
  }

  cancel() {
    this.dialogRef.close(false);
  }

  submitForm() {
    this.departmentClient
      .departmentPOST({
        name: this.addDepartmentForm.controls['name'].value,
        abbreviation: this.addDepartmentForm.controls['abbreviation'].value,
      })
      .subscribe(() => {
        this.dialogRef.close(true);
      });
  }

  public errorHandling = (control: string, error: string) => {
    return this.addDepartmentForm.controls[control].hasError(error);
  };
}
