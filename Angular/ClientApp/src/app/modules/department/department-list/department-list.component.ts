import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { DepartmentAddComponent } from '../department-add/department-add.component';
import { DepartmentEditComponent } from '../department-edit/department-edit.component';
import {
  MatSnackBar,
  MatSnackBarHorizontalPosition,
  MatSnackBarVerticalPosition,
} from '@angular/material/snack-bar';
import { DepartmentClient, DepartmentDto } from 'src/app/api/app.generated';

@Component({
  selector: 'app-department-list',
  templateUrl: './department-list.component.html',
  styleUrls: ['./department-list.component.css'],
})
export class DepartmentListComponent implements OnInit {
  departments: DepartmentDto[] = [];

  columnsToDisplay = ['name', 'abbreviation', 'actions'];
  horizontalPosition: MatSnackBarHorizontalPosition = 'end';
  verticalPosition: MatSnackBarVerticalPosition = 'top';

  constructor(
    private readonly departmentClient: DepartmentClient,
    private readonly dialog: MatDialog,
    private readonly snackBar: MatSnackBar
  ) {}

  ngOnInit(): void {
    this.loadData();
  }

  loadData(): void {
    this.departmentClient.departmentAll().subscribe((data) => {
      this.departments = data;
    });
  }

  addDepartment() {
    const dialogRef = this.dialog.open(DepartmentAddComponent, {
      disableClose: true,
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.snackBar.open('Success', 'Department create success', {
          duration: 500,
          horizontalPosition: this.horizontalPosition,
          verticalPosition: this.verticalPosition,
        });
        this.loadData();
      }
    });
  }

  deleteDepartment(departmentId: number) {
    this.departmentClient.departmentDELETE(departmentId).subscribe(() => {
      this.snackBar.open('Success', 'Department delete success', {
        duration: 500,
        horizontalPosition: this.horizontalPosition,
        verticalPosition: this.verticalPosition,
      });
      this.loadData();
    });
  }

  editDepartment(department: DepartmentDto) {
    const dialogRef = this.dialog.open(DepartmentEditComponent, {
      data: department,
      disableClose: true,
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.snackBar.open('Success', 'Department edit success', {
          duration: 500,
          horizontalPosition: this.horizontalPosition,
          verticalPosition: this.verticalPosition,
        });
        this.loadData();
      }
    });
  }
}
