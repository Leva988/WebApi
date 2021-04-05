import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { User } from 'src/app/models/user';

@Injectable()
export class BackEndService {

  usersUrl = environment.backendUrl + '/Users';
  constructor(private http: HttpClient ) {
      }

    getUsers(): Observable<User[]> {
      return this.http.get<User[]>(this.usersUrl , { responseType: 'json'});
    }
}
