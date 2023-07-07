import RegisterModel from "models/RegisterModel";
import axios, { AxiosError } from 'axios';
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

  public async registerUser(model: RegisterModel): Promise<Result<string, AxiosError>> {
  
    console.table(model);
  
    try {
      await axios.post(`${this._baseUrl}/user/register`, model)
      return {success: true, value: "User registered successfully"};
    } catch (error: any) {
      console.log("Couldn't register", error.message);
      return {success: false, error: error as AxiosError};
    }
  
  }
}