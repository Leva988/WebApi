import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { User } from 'src/app/models/user';
import { Company } from 'src/app/models/company';

@Injectable()
export class BackEndService {

  usersUrl = environment.backendUrl + '/Users';
  companiesUrl = environment.backendUrl + '/Companies';
  constructor(private http: HttpClient ) {
      }

    getUsers(): Observable<User[]> {
      return this.http.get<User[]>(this.usersUrl , { responseType: 'json'});
    }

    getCompanies(): Observable<Company[]> {
      return this.http.get<Company[]>(this.companiesUrl , { responseType: 'json'});
    }

    addUser(user: User) {
      return this.http.post(this.usersUrl, user);
    }

    updateUser(id: string, user: User) {
      delete user.id;
      return this.http.put(this.usersUrl + '/' + id, user);
    }

    deleteUser(id: number) {
      return this.http.delete(this.usersUrl + '/' + id);
    }
}
