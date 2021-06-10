import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'clientapp';

  userLoggedIn:boolean = false;
  cName? : string = '';
  role? : string='';

  constructor( private router : Router)
  {
    window.scroll(0, 0);

    if (localStorage.getItem('access_token') == null)
    {
      this.userLoggedIn = false;
      this.router.navigate(['/login']);

    }else{
      this.userLoggedIn = true;
      this.GetUserInfo();
      this.router.navigate(['/dashboard']);
    }

  }

  public GetUserInfo()
  {
    this.cName = localStorage.getItem('cname')?.toString();
    this.role = localStorage.getItem('role')?.toString();
  }
}
