import { makeAutoObservable } from "mobx";
import axios from "axios";

class ApiClientStore {
  

  constructor(){
    makeAutoObservable(this);
  }
  
}

export { ApiClientStore };
