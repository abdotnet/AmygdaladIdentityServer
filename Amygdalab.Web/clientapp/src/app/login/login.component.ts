import { Login, LoginResponse } from './../_model/model';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from '../_service/auth.service';
import {JwtHelperService} from '@auth0/angular-jwt';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  public model={
    userName: "",
    password :""
  };

   helper  = new JwtHelperService();

  constructor(private authService : AuthService,
    private toastr: ToastrService,
    private router : Router , private spinner : NgxSpinnerService
    ) {
      window.scroll(0, 0);
    }

  ngOnInit(): void {
  }

  public Login(login : Login)
  {
    if (login == null)
     {
      this.toastr.error('Bad request..Either username or password is empty' );
      return;
     }
   var json = JSON.stringify(login);

   this.spinner.show();
  this.authService.LoginUser(login).subscribe(c=>{
    this.spinner.hide();
    var resp = <LoginResponse> c;

    if (resp != null)
       {
         const decode = this.helper.decodeToken(resp.access_token);
         localStorage.setItem('access_token',resp.access_token);
         localStorage.setItem('refresh_token',resp.refresh_token);
        // var xx = JSON.stringify(decode);
         var token = this.parseJwt(resp.access_token);

         var tkn = localStorage.getItem('role')?.toString();
         console.log(tkn);
         if (tkn == null)
         {
          localStorage.setItem('role',token.role);
         }

         var cname  = localStorage.getItem('cName')?.toString();
         console.log(cname);
         if (cname == null)
         {
          localStorage.setItem('cName',token.Firstname +' '+ token.Lastname);
         }
         var userId  = localStorage.getItem('userId')?.toString();
         console.log(userId);
         if(userId == null)
         {
          localStorage.setItem('userId',token.userId);
         }

         this.toastr.success('Successully created');
         
         this.authService.GetUserInfo();

         this.router.navigate(['/app']);
       }
     else
     {
      this.spinner.hide();
          this.toastr.error('Login failed');
     }
  },error => {
    this.spinner.hide();
    this.toastr.error('Login failed: An error occured!' );
  });
  }

  parseJwt(token: string) {
    var base64Url = token.split('.')[1];
    var base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
    var jsonPayload = decodeURIComponent(atob(base64).split('').map(function (c) {
        return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
    }).join(''));

    return JSON.parse(jsonPayload);
};

}
