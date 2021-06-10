import { AuthService } from './../_service/auth.service';
import { Component, OnInit } from '@angular/core';
import { ApiResponse, IRegister } from '../_model/model';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  public model=  {
    userName:"",
    firstName: "",
    lastName: "",
    password: "",
    phoneNumber: "",
    emailAddress: "",
    role:0
  };
 result : any;
  constructor(private authService : AuthService,
    private toastr: ToastrService, private spinner : NgxSpinnerService) {
    window.scroll(0, 0);
  }

  ngOnInit(): void {
  }

  public register(register: IRegister)
  {

    this.spinner.show();
   this.authService.RegisterWorker(register).subscribe((c )=>{
    this.spinner.hide();
     var resp = <ApiResponse<object>> c;
     console.log(resp);
     if (resp.requestSuccessful)
        {
          this.toastr.success('Successully created');
          register =this.result;
        }
      else
      {
           this.toastr.error('registration failed');
      }
   });
  }

}
