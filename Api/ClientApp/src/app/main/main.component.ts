import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { User } from '../models/user';
import { BackEndService } from './service/backend.service';
import {  GridOptions, GridReadyEvent } from 'ag-grid-community';
import { ButtonRendererComponent } from '../button-renderer.component';
import { NgForm } from '@angular/forms';
import { Company } from '../models/company';


declare var $: any;

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss'],
  providers: [BackEndService]
})
export class MainComponent implements OnInit {

  users: Observable<User[]>;
  companies: Observable<Company[]>;
  editUser: User = new User();
  columnDefs: any[] = [];
  rowData: any[] = [];
  gridOptions: GridOptions;
  editID: string;
  isNewRecord: boolean;
  modalColor: string;
  modalTitle: string;
  modalMessage: string;

  constructor(private service: BackEndService) { }

  ngOnInit() {
    $('#modalMessage').hide();
    $('#addModal').on('hidden.bs.modal', () => {
        this.modalMessage = this.modalColor = this.modalTitle = null;
        $('#modalMessage').hide();
    });
    this.getUsers();
    this.companies = this.service.getCompanies();
    this.columnDefs = [
      { field: 'id', headerName: 'Id', sortable: true, filter: true, resizable: true },
      { field: 'name', headerName: 'Имя', sortable: true, filter: true, resizable: true },
      { field: 'age', headerName: 'Возраст', sortable: true, filter: true, resizable: true },
      { field: 'activity', headerName: 'Должность', sortable: true, filter: true, resizable: true },
      { field: 'company', headerName: 'Компания', sortable: true, filter: true, resizable: true },
      {
        headerName: '',
        cellRenderer: 'buttonRenderer',
        cellRendererParams: {
        onClick: this.updateUser.bind(this),
            label: 'Изменить',
            class: 'btn btn-secondary',
            modal: '#addModal',
            maxWidth: 100,
        },
        resizable: true
      },
      {
        headerName: '',
        cellRenderer: 'buttonRenderer',
        cellRendererParams: {
          onClick: this.deleteUser.bind(this),
          label: 'Удалить',
          class: 'btn btn-danger',
          modal: ''
        },
        resizable: true
      }
    ];
    this.gridOptions = {
      context: {parentComponent: this},
      frameworkComponents: {
        buttonRenderer: ButtonRendererComponent
      },
      pagination: true,
      paginationPageSize: 10,
      rowSelection: 'single',
      onGridReady: (ev: GridReadyEvent) => {
          ev.api.sizeColumnsToFit();
      }
     };
  }
  getUsers() {
   this.rowData = [];
   this.users = this.service.getUsers();
   this.users.subscribe(
      (data: User[]) => {
      this.rowData = data;
    }, (error) => console.log(error)
   );
  }

  addUser() {
    this.editUser = new User();
    this.modalTitle = 'Добавить пользователя';
    this.isNewRecord = true;
  }

  updateUser(e) {
    this.editID = e.rowData.id;
    this.editUser = e.rowData;
    this.modalTitle = 'Изменить пользователя';
    this.isNewRecord = false;
  }

  saveUser(form: NgForm) {
    if (form.valid) {
    if (this.isNewRecord) {
        this.service.addUser(this.editUser).subscribe(
            () => {
                this.modalColor = '#2fc900';
                this.modalMessage = `Пользователь добавлен`;
                this.getUsers();
                $('#modalMessage').show();
            },
            error => {
                this.modalMessage = 'Введите верные данные!';
                this.modalColor = '#f20800';
                $('#modalMessage').show();
                console.log(error);
             });
        } else {
            this.service.updateUser(this.editID, this.editUser).subscribe(
                () => {
                    this.modalColor = '#2fc900';
                    this.modalMessage = `Данные по проекту обновлены`;
                    this.getUsers();
                    $('#modalMessage').show();
                },
                error => {
                    this.modalMessage = 'Введите верные данные!';
                    this.modalColor = '#f20800';
                    $('#modalMessage').show();
                    console.log(error);
                 });
        }
    } else {
        this.modalMessage = 'Введите данные!';
        this.modalColor = '#f20800';
        $('#modalMessage').show();
    }
}

cancel() {
    $('#modalMessage').hide();
    this.editUser = new User();
    if (this.isNewRecord) {
        this.isNewRecord = false;
    }
}

deleteUser(e) {
    if (confirm('Удалить пользователя?')) {
        const deleteId = e.rowData.id;
        this.service.deleteUser(deleteId).subscribe(
             () => {
                 this.getUsers();
              },
              error => {
                 console.log(error);
                });
    }
 }
}
