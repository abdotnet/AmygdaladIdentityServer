import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject } from 'rxjs';
import { AuthService } from '../_service/auth.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

  cName? = new  BehaviorSubject<string|undefined>('');
  role? = new  BehaviorSubject<string|undefined>('');

  constructor(  private router : Router , private auth : AuthService) {
    window.scroll(0, 0);
  }

  ngOnInit(): void {

    this.auth.GetUserInfo();
    this.cName = this.auth.cName;
    this.role = this.auth.role;
    console.log(this.auth.cName +' '+ this.auth.role);
  }

  public Logout()
  {
    localStorage.removeItem('access_token');
    localStorage.removeItem('refresh_token');
    localStorage.removeItem('role');
    localStorage.removeItem('cName');
    localStorage.removeItem('userId');
    localStorage.clear();
    this.auth.GetUserInfo();
    this.router.navigate(['/login']);
  }



}
