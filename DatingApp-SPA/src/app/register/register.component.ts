import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { error } from 'protractor';
import { AlertifyService } from '../_services/alertify.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  
  @Output() cancleRegister = new EventEmitter();



  model: any = {}; //empty object

  constructor(private authservice:AuthService, private alertify:AlertifyService) { }

  ngOnInit() {
  }

  register()
  {
    this.authservice.register(this.model).subscribe(() => {
      //console.log('registration sucessful');
      this.alertify.success('Registration is successful');
    }, error =>{
      //console.log(error);
      this.alertify.error('Registration error');
      this.alertify.error(error);
    }
    )
  }

  cancle(){
    this.cancleRegister.emit(false);
    //console.log("cancelled");
    this.alertify.error('Registration cancelled');
  }

}
