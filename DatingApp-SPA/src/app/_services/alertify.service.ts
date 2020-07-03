import { Injectable } from '@angular/core';
import * as alertify from 'alertifyjs';

@Injectable({
  providedIn: 'root'
})
export class AlertifyService {

  constructor() { }

  //Confirm method, which will be called on the frontpage of the app
  //message is a string type
  //okCallback is a function parameter of type any
  confirm(message: string, okCallback: () => any) {
    alertify.confirm(message, (e: any) => {
      if (e) {
        okCallback();
      } else {
        //keeping it empty, as we are not going to do anything, if the event is not found for any callback function

      }
    })
  }

  //Success method, which will be called on the frontpage of the app
  //message is a string type
  success(message: string) {
    alertify.success(message);
  }

  //Error method, which will be called on the frontpage of the app
  //message is a string type
  error(message: string) {
    alertify.error(message);
  }

  //Warning method, which will be called on the frontpage of the app
  //message is a string type
  warning(message: string) {
    alertify.warning(message);
  }

  //Message method, which will be called on the frontpage of the app
  //message is a string type
  message(message: string) {
    alertify.message(message);
  }

}
