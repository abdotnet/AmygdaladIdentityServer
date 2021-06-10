import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import {  IRegister, Login } from '../_model/model';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

 public  cName  = new BehaviorSubject<string | undefined>('');
 public role = new BehaviorSubject<string | undefined>('');

  public GetUserInfo()
  {

    let name  =  localStorage.getItem('cName')?.toString();
    let _role = localStorage.getItem('role')?.toString();

    this.cName.next(name);
    this.role.next(_role) ;
  }

  private baseURL = environment.serverUrl;
  private headers = new HttpHeaders(
    {
        'Content-Type':  'application/x-www-form-urlencoded',
        Accept: '*/*',
    });

     httpOptions = {
      headers: new HttpHeaders({ 'Content-Type': 'application/x-www-form-urlencoded' }),
    };
  constructor(private http: HttpClient) { }

  public RegisterWorker(register: IRegister) :Observable<object> {
    var result =  this.http.post(environment.serverUrl + '/api/v1/account/user', register);
    return result;
  }

  public RegisterAdmin(object: any) {
    return this.http.post(environment.serverUrl + '/api/v1/account/admin', object);
  }
  public LoginUser(login: Login) {

    var params = new HttpParams({
      fromObject: { username: login.userName, password: login.password,
         client_id:'ro.angular',grant_type: 'password',client_secret: 'secret'},
    });

     var url = environment.serverUrl + '/connect/token';

    return this.http.post<any>(url, params.toString(),this.httpOptions)
    .pipe(map(res => {
      console.log(res);
      return res;
    }));
  }

}
