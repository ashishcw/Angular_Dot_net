import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';


@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  //adding the other service refrence to check if the user is logged in or not, before showing them the desired page.

  constructor(
    private authService: AuthService, 
    private router: Router,
    private alertify: AlertifyService
    ){};

  canActivate(): boolean{
    if(this.authService.loggedIn())
    {
      return true;
    }

    this.alertify.error('You must be logged in to access this page.');
    this.router.navigate(['/home']);
    return false;
  }

}
