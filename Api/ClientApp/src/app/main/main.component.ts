import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { User } from '../models/user';
import { BackEndService } from './service/backend.service';
import {  GridOptions, GridReadyEvent } from 'ag-grid-community';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss'],
  providers: [BackEndService]
})
export class MainComponent implements OnInit {

  title = 'User Page';
  users: Observable<User[]>;
  columnDefs: any[] = [];
  rowData: any[] = [];
  gridOptions: GridOptions;

  constructor(private service: BackEndService) { }

  ngOnInit() {
    this.getUsers();
  }

  getUsers() {
   this.users = this.service.getUsers();
   this.users.subscribe(
    (data: User[]) => {
      this.rowData = data;
      console.log(this.rowData);
      this.columnDefs = [
        { field: 'id', headerName: 'Id', sortable: true, filter: true, resizable: true },
        { field: 'name', headerName: 'Имя', sortable: true, filter: true, resizable: true },
        { field: 'age', headerName: 'Возраст', sortable: true, filter: true, resizable: true },
        { field: 'activity', headerName: 'Должность', sortable: true, filter: true, resizable: true },
        { field: 'company', headerName: 'Компания', sortable: true, filter: true, resizable: true }
      ];
      this.gridOptions = {
        context: {parentComponent: this},
        pagination: true,
        paginationPageSize: 10,
        rowSelection: 'single',
        onGridReady: (ev: GridReadyEvent) => {
            ev.api.sizeColumnsToFit();
        }
       };
    }
   );
  }

}
