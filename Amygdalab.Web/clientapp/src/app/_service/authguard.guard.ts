import { Injectable } from '@angular/core';
import { ActivatedRoute, ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class AuthguardGuard implements CanActivate {
  constructor(private authService: AuthService, private router: Router) {}

  canActivate(next: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    try
    {
      var menuRole = next.data['role']
      var userRole = localStorage.getItem('role');

      if (menuRole == "WorkerAdmin")
         return true;

         if (menuRole ==  userRole)
         return true;


    }
    catch{

    }


    if (localStorage.getItem("access_token") !=null) {
      return true;
    }

    this.router.navigate(['/login']);
    return false;
  }

}
