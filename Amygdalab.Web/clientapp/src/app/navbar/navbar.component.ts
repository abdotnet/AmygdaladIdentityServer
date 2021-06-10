import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {

  cName? : string = '';
  role? : string='';

  constructor(  private router : Router) {
    window.scroll(0, 0);
  }

  ngOnInit(): void {

    this.GetUserInfo();
  }

  public Logout()
  {
    localStorage.removeItem('access_token');
    localStorage.removeItem('refresh_token');
    localStorage.removeItem('role');
    localStorage.removeItem('cname');
    localStorage.removeItem('userId');
    localStorage.clear();

    this.router.navigate(['/login']);
  }

  public GetUserInfo()
  {
    this.cName = localStorage.getItem('cname')?.toString();
    this.role = localStorage.getItem('role')?.toString();
    console.log(this.cName +' '+ this.role);
  }

}
