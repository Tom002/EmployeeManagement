import { Component, Inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { combineLatest } from 'rxjs';
import {
  EmployeeListDto,
  DepartmentDto,
  EmployeeClient,
  DepartmentClient,
  EmployeeUpdateDto,
  EmployeeDetailsDto,
} from 'src/app/api/app.generated';
import { EmployeeAddComponent } from '../employee-add/employee-add.component';

@Component({
  selector: 'app-employee-edit',
  templateUrl: './employee-edit.component.html',
  styleUrls: ['./employee-edit.component.css'],
})
export class EmployeeEditComponent {
  editEmployeeForm!: FormGroup;
  bossSelectOptions: EmployeeListDto[] = [];
  departmentSelectOptions: DepartmentDto[] = [];
  hide = true;
  dataLoaded = false;
  employeeId!: number;
  employeeData!: EmployeeDetailsDto;

  constructor(
    private readonly employeeClient: EmployeeClient,
    private readonly departmentClient: DepartmentClient,
    private readonly dialogRef: MatDialogRef<EmployeeAddComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private readonly fb: FormBuilder
  ) {
    this.employeeId = data.id;
  }

  ngOnInit(): void {
    combineLatest([
      this.employeeClient.employeeAll(),
      this.departmentClient.departmentAll(),
      this.employeeClient.employeeGET(this.employeeId),
    ]).subscribe((result) => {
      this.dataLoaded = true;

      // Saját maga ne lehessen a főnöke
      (this.bossSelectOptions = result[0].filter(
        (x) => x.id !== this.employeeId
      )),
        (this.departmentSelectOptions = result[1]);
      this.employeeData = result[2];
      this.initForm();
    });
  }

  initForm() {
    this.editEmployeeForm = this.fb.group({
      name: [
        this.employeeData.name,
        [Validators.required, Validators.maxLength(100)],
      ],
      position: [
        this.employeeData.position,
        [Validators.required, Validators.maxLength(100)],
      ],
      phone: [
        this.employeeData.phone,
        [
          Validators.required,
          Validators.maxLength(16),
          Validators.pattern('^((\\+36-?))?[0-9]{8,10}$'),
        ],
      ],
      bossId: [this.employeeData.bossId ?? undefined],
      departmentId: [this.employeeData.departmentId, [Validators.required]],
    });
  }

  cancel() {
    this.dialogRef.close(false);
  }

  submitForm() {
    let updateDto = {
      name: this.editEmployeeForm.controls['name'].value,
      position: this.editEmployeeForm.controls['position'].value,
      phone: this.editEmployeeForm.controls['phone'].value,
      departmentId: this.editEmployeeForm.controls['departmentId'].value,
    } as EmployeeUpdateDto;

    if (this.editEmployeeForm.controls['bossId'].value) {
      updateDto.bossId = this.editEmployeeForm.controls['bossId'].value;
    }

    this.employeeClient
      .employeePUT(this.employeeId, updateDto)
      .subscribe(() => {
        this.dialogRef.close(true);
      });
  }

  public errorHandling = (control: string, error: string) => {
    return this.editEmployeeForm.controls[control].hasError(error);
  };
}
