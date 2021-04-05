import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { User } from '../models/user';
import { BackEndService } from './service/backend.service';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss'],
  providers: [BackEndService]
})
export class MainComponent implements OnInit {

  title = 'User Page';
  users: Observable<User[]>;
  constructor(private service: BackEndService) { }

  ngOnInit() {
    this.getUsers();
  }

  getUsers() {
   this.users = this.service.getUsers();
   console.log(this.users);
  }

}
