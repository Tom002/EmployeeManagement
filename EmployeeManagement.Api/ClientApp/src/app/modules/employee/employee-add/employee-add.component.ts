import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { combineLatest } from 'rxjs';
import {
  DepartmentClient,
  DepartmentDto,
  EmployeeClient,
  EmployeeCreateDto,
  EmployeeListDto,
} from 'src/app/api/app.generated';

@Component({
  selector: 'app-employee-add',
  templateUrl: './employee-add.component.html',
  styleUrls: ['./employee-add.component.css'],
})
export class EmployeeAddComponent implements OnInit {
  addEmployeeForm!: FormGroup;

  bossSelectOptions: EmployeeListDto[] = [];
  departmentSelectOptions: DepartmentDto[] = [];
  hide = true;
  dataLoaded = false;

  constructor(
    private readonly employeeClient: EmployeeClient,
    private readonly departmentClient: DepartmentClient,
    private readonly dialogRef: MatDialogRef<EmployeeAddComponent>,
    private readonly fb: FormBuilder
  ) {
    this.initForm();
  }

  ngOnInit(): void {
    combineLatest([
      this.employeeClient.employeeAll(),
      this.departmentClient.departmentAll(),
    ]).subscribe((result) => {
      this.dataLoaded = true;
      this.bossSelectOptions = result[0];
      this.departmentSelectOptions = result[1];
    });
  }

  initForm() {
    this.addEmployeeForm = this.fb.group({
      username: ['', [Validators.required, Validators.maxLength(100)]],
      password: ['', [Validators.required, Validators.minLength(8)]],
      name: ['', [Validators.required, Validators.maxLength(100)]],
      position: ['', [Validators.required, Validators.maxLength(100)]],
      phone: [
        '',
        [
          Validators.required,
          Validators.maxLength(16),
          Validators.pattern('^((\\+36-?))?[0-9]{8,10}$'),
        ],
      ],
      bossId: [undefined],
      departmentId: [undefined, [Validators.required]],
    });
  }

  cancel() {
    this.dialogRef.close(false);
  }

  submitForm() {
    var createDto = {
      username: this.addEmployeeForm.controls['username'].value,
      password: this.addEmployeeForm.controls['password'].value,
      name: this.addEmployeeForm.controls['name'].value,
      position: this.addEmployeeForm.controls['position'].value,
      phone: this.addEmployeeForm.controls['phone'].value,
      departmentId: this.addEmployeeForm.controls['departmentId'].value,
    } as EmployeeCreateDto;

    if (this.addEmployeeForm.controls['bossId'].value) {
      createDto.bossId = this.addEmployeeForm.controls['bossId'].value;
    }

    this.employeeClient.employeePOST(createDto).subscribe(() => {
      this.dialogRef.close(true);
    });
  }

  public errorHandling = (control: string, error: string) => {
    return this.addEmployeeForm.controls[control].hasError(error);
  };
}
