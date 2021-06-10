import { ProductService } from './../_service/product.service';
 export interface IRegister
 {
  userName:string;
  firstName: string;
  lastName: string;
  password: string;
  phoneNumber: string;
  emailAddress: string;
}
export interface LoginResponse
{
  access_token : string;
  expires_in : number;
  token_type : string;
  refresh_token : string;
  scope : string;
}

 export interface ApiResponse<T>
 {
  responseData: T;
  responseMessage : string;
  responseCode : string;
  requestSuccessful : boolean;
 }

 export interface Login{

  userName : string;
  password : string;
 }

 export interface Product{
  name : string;
  description : string;
  costPrice : number;
  sellingPrice : number;
  imageUrl : number;
  quantity : number;

  }
 export interface Pager<T>
{
  currentPage : number,
  itemsPerPage : number,
  totalItems : number,
  totalPages : number,
  result:Array<T>
}
  export interface ProductResponse
  {
    id : number;
    name : string;
    description : string;
    costPrice :number;
    sellingPrice : number;
    imageUrl : string;
    quantity : number;
    createdOn:Date;
  }

