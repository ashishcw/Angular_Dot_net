import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';
import { watchFile } from 'fs';
import { Router } from '@angular/router';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

  model:any = {}; //empty object

  //adding alertify service
  constructor(public authService:AuthService, private alertify:AlertifyService, private router:Router) { }

  ngOnInit(): void {
  }

  login(){
    //console.log(this.model);
    this.authService.login(this.model).subscribe(next => {
      //console.log('Logged in successfully')
      this.alertify.success('Logged in successfully');
      this.router.navigate(['/members']);
    }, error => {
      //console.log(error);
      this.alertify.error(error);
    })
  }

  loggedIn(){
    return this.authService.loggedIn();
    //Since these below lines have been implemented inside the auth service
    // const token = localStorage.getItem('token');
    // return !!token;
  }

  logOut(){    
    localStorage.removeItem('token');
    this.alertify.message('Logged out successfully');
    this.router.navigate(['/home']);
  }

}
