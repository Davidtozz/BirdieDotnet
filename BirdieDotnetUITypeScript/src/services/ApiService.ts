import RegisterModel from "models/RegisterModel";
import axios, { AxiosError, AxiosResponse } from 'axios';
import Result from "types/Result";
import LoginModel from "models/LoginModel";

axios.defaults.withCredentials = true;


export default class ApiService {
  
  private static _instance: ApiService;
  private _baseUrl: string = "http://localhost:5069/api";
  
  private constructor() {}
  
  public static get instance(): ApiService {
    if(!ApiService._instance) {
      ApiService._instance = new ApiService();
    }
    return ApiService._instance;
  }


  private async postRequest<T>(
    endpoint: string,
    model: T
  ): Promise<Result<AxiosResponse, AxiosError>> {
    //? test if the model is being passed in correctly
    /* console.table(model); */
    try {
      const response: AxiosResponse = await axios.post(`${this._baseUrl}${endpoint}`, model)
      return {success: true, value: response};
    } catch (error: any) {
      console.log("Couldn't register", error.message);
      return {success: false, error: error as AxiosError};
    }
  }

  private async getRequest(
    endpoint: string
  ): Promise<Result<AxiosResponse, AxiosError>> {
    //? test if the model is being passed in correctly
    /* console.table(model); */
    try {
      const response: AxiosResponse = await axios.get(`${this._baseUrl}${endpoint}`, )
      return {success: true, value: response};
    } catch (error: any) {
      console.log("Couldn't register", error.message);
      return {success: false, error: error as AxiosError};
    }
  }


  public async refreshToken(): Promise<Result<AxiosResponse, AxiosError>> {
    return await this.postRequest("/user/refresh", {});
  }

  public async getUsers(): Promise<Result<AxiosResponse, AxiosError>> {
    return await this.getRequest("/user");
  }

  public async registerUser(model: RegisterModel): Promise<Result<AxiosResponse, AxiosError>> {
    return await this.postRequest<RegisterModel>("/user/register", model);
  }

  public async loginUser(model: LoginModel): Promise<Result<AxiosResponse, AxiosError>> {
    return await this.postRequest<LoginModel>("/user/login", model);
  }

}