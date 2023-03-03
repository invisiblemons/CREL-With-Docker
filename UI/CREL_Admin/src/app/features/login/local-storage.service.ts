import { Injectable } from '@angular/core';
import { Admin } from '../admin/admin.model';

const AUTH = "authenticate";
@Injectable({
  providedIn: 'root'
})
export class LocalStorageService {
  constructor() { }

  setUserObject(user) {
    let auth = JSON.parse(localStorage.getItem(AUTH));
    auth['user'] = user;
    localStorage.setItem(AUTH, JSON.stringify(auth))
  }
  setUser(authenticate) {
    localStorage.setItem(AUTH, JSON.stringify(authenticate))
  }
  getUserToken() : string{
    let auth = JSON.parse(localStorage.getItem(AUTH))
    return auth?auth['token']:null;
  }
  getUserObject() : Admin{
    let auth = JSON.parse(localStorage.getItem(AUTH))
    return auth?auth['user']:null;
  }
  removeUser()
  {
    return localStorage.removeItem(AUTH);
  }
}
