import { Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { EmployeeClient, EmployeeListDto } from 'src/app/api/app.generated';
import { EmployeeAddComponent } from '../employee-add/employee-add.component';
import { EmployeeEditComponent } from '../employee-edit/employee-edit.component';
import {
  MatSnackBar,
  MatSnackBarHorizontalPosition,
  MatSnackBarVerticalPosition,
} from '@angular/material/snack-bar';

@Component({
  selector: 'app-employee-list',
  templateUrl: './employee-list.component.html',
  styleUrls: ['./employee-list.component.css'],
})
export class EmployeeListComponent {
  employees: EmployeeListDto[] = [];

  columnsToDisplay = ['name', 'position', 'phone', 'departmentName', 'actions'];
  horizontalPosition: MatSnackBarHorizontalPosition = 'end';
  verticalPosition: MatSnackBarVerticalPosition = 'top';

  constructor(
    private readonly employeeClient: EmployeeClient,
    private readonly dialog: MatDialog,
    private readonly snackBar: MatSnackBar
  ) {}
  ngOnInit(): void {
    this.loadData();
  }

  loadData(): void {
    this.employeeClient.employeeAll().subscribe((data) => {
      this.employees = data;
    });
  }

  addEmployee() {
    const dialogRef = this.dialog.open(EmployeeAddComponent, {
      disableClose: true,
    });
    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.snackBar.open('Success', 'Employee create success', {
          duration: 500,
          horizontalPosition: this.horizontalPosition,
          verticalPosition: this.verticalPosition,
        });
        this.loadData();
      }
    });
  }

  deleteEmployee(employeeId: number) {
    this.employeeClient.employeeDELETE(employeeId).subscribe(() => {
      this.snackBar.open('Success', 'Employee delete success', {
        duration: 500,
        horizontalPosition: this.horizontalPosition,
        verticalPosition: this.verticalPosition,
      });
      this.loadData();
    });
  }

  editEmployee(employeeId: number) {
    const dialogRef = this.dialog.open(EmployeeEditComponent, {
      data: { id: employeeId },
      disableClose: true,
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.snackBar.open('Success', 'Employee edit success', {
          duration: 500,
          horizontalPosition: this.horizontalPosition,
          verticalPosition: this.verticalPosition,
        });
        this.loadData();
      }
    });
  }
}
