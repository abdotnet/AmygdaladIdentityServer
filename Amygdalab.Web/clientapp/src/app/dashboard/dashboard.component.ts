import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {

  userLoggedIn:boolean = false;
  cName? : string = '';
  role? : string='';
  constructor() { }

  ngOnInit(): void {

    if (localStorage.getItem('access_token') != null)
    {
      this.userLoggedIn = true;
      this.GetUserInfo();
    }
    else
    {
      this.userLoggedIn = false;
    }
  }

  public GetUserInfo()
  {
    this.cName = localStorage.getItem('cname')?.toString();
    this.role = localStorage.getItem('role')?.toString();
  }

}
