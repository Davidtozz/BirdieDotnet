import RegisterModel from "models/RegisterModel";
import axios from 'axios';
import Result from "types/Result";


export default class ApiService {
  
  private static _instance: ApiService | null = null;
  private _baseUrl: string = "http://localhost:5069/api";
  
  private constructor() {}
  
  public static get instance(): ApiService {
    if(!ApiService._instance) {
      ApiService._instance = new ApiService();
    }
    return ApiService._instance;
  }

  public async registerUser(data: RegisterModel): Promise<void> {
    
    console.log(data);

    fetch(`${this._baseUrl}/user/register`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
        
      },
      body: JSON.stringify(data)
    })
    .then(response => response.json())
    .catch(error => console.log("Couldn't register", error));

   /*  try {
      await axios.post(`${this._baseUrl}/user/register`, data)
    } catch (error) {
      console.log("Couldn't register", error);


    } */
  
  }
}