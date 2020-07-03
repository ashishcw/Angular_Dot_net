import { Component } from '@angular/core';
import {JwtHelperService} from '@auth0/angular-jwt';
import { AuthService } from './_services/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'DatingApp-SPA';

  jwtHelper = new JwtHelperService();

  constructor(private authService: AuthService){}

  ngOnInit()
  {
    const token = localStorage.getItem('token');
    if(token != null)
    {
      this.authService.decodedToken = this.jwtHelper.decodeToken(token);
      this.authService.username = this.authService.decodedToken.unique_name;
    }
  }


}
