import RegisterModel from "models/RegisterModel";
import axios, { AxiosError, AxiosResponse } from 'axios';
import Result from "types/Result";
import LoginModel from "models/LoginModel";

axios.defaults.withCredentials = true;


/**
 * Service class for making API requests.
 */
export default class ApiService {
  /**
   * Singleton instance of ApiService.
   */
  private static _instance: ApiService;
  
  /**
   * Base URL for API requests.
   */
  private _baseUrl: string = "http://localhost:5069/api";
  
  /**
   * Private constructor to enforce singleton pattern.
   */
  private constructor() {}
  
  /**
   * Get the singleton instance of ApiService.
   * @returns {ApiService} The singleton instance.
   */
  public static get instance(): ApiService {
    if (!ApiService._instance) {
      ApiService._instance = new ApiService();
    }
    return ApiService._instance;
  }

  /**
   * Perform a POST request to the specified endpoint.
   * @template T - The type of the model being sent in the request body.
   * @param {string} endpoint - The endpoint to send the request to.
   * @param {T} model - The model to send in the request body.
   * @returns {Promise<Result<AxiosResponse, AxiosError>>} A promise that resolves to the result of the request.
   */
  private async postRequest<T>(
    endpoint: string,
    model: T
  ): Promise<Result<AxiosResponse, AxiosError>> {
    //? test if the model is being passed in correctly
    /* console.table(model); */
    try {
      const response: AxiosResponse = await axios.post(`${this._baseUrl}${endpoint}`, model)
      return { success: true, value: response };
    } catch (error: any) {
      console.log("Couldn't register", error.message);
      return { success: false, error: error as AxiosError };
    }
  }

  /**
   * Perform a GET request to the specified endpoint.
   * @param {string} endpoint - The endpoint to send the request to.
   * @returns {Promise<Result<AxiosResponse, AxiosError>>} A promise that resolves to the result of the request.
   */
  private async getRequest(
    endpoint: string
  ): Promise<Result<AxiosResponse, AxiosError>> {
    //? test if the model is being passed in correctly
    /* console.table(model); */
    try {
      const response: AxiosResponse = await axios.get(`${this._baseUrl}${endpoint}`)
      return { success: true, value: response };
    } catch (error: any) {
      console.log("Couldn't register", error.message);
      return { success: false, error: error as AxiosError };
    }
  }

  /**
   * Refresh the user's token.
   * @returns {Promise<Result<AxiosResponse, AxiosError>>} A promise that resolves to the result of the request.
   */
  public async refreshToken(): Promise<Result<AxiosResponse, AxiosError>> {
    return await this.postRequest("/user/refresh", {});
  }

  /**
   * Get a list of users.
   * @returns {Promise<Result<AxiosResponse, AxiosError>>} A promise that resolves to the result of the request.
   */
  public async getUsers(): Promise<Result<AxiosResponse, AxiosError>> {
    return await this.getRequest("/user");
  }

  /**
   * Register a new user.
   * @param {RegisterModel} model - The registration model.
   * @returns {Promise<Result<AxiosResponse, AxiosError>>} A promise that resolves to the result of the request.
   */
  public async registerUser(model: RegisterModel): Promise<Result<AxiosResponse, AxiosError>> {
    return await this.postRequest<RegisterModel>("/user/register", model);
  }

  /**
   * Log in a user.
   * @param {LoginModel} model - The login model.
   * @returns {Promise<Result<AxiosResponse, AxiosError>>} A promise that resolves to the result of the request.
   */
  public async loginUser(model: LoginModel): Promise<Result<AxiosResponse, AxiosError>> {
    return await this.postRequest<LoginModel>("/user/login", model);
  }
}
