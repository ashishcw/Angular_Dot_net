import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { error } from 'protractor';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  
  @Output() cancleRegister = new EventEmitter();



  model: any = {}; //empty object

  constructor(private authservice:AuthService) { }

  ngOnInit() {
  }

  register()
  {
    this.authservice.register(this.model).subscribe(() => {
      console.log('registration sucessful');
    }, error =>{
      console.log(error);
    }
    )
  }

  cancle(){
    this.cancleRegister.emit(false);
    console.log("cancelled");
  }

}
