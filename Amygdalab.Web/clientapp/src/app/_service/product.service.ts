import { Observable } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Product } from '../_model/model';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  private baseURL = environment.serverUrl;

   bearer : string ='Bearer ' + localStorage.getItem('access_token')?.toString();
  private headers = new HttpHeaders(
    {
        'Content-Type':  'application/json',
        Accept: '*/*',
        'Authorization':this.bearer
    });

  constructor(private http: HttpClient) { }

  public createproduct(product: Product) :Observable<object> {
    console.log('Bearer token '+ this.bearer);
    return this.http.post(environment.serverUrl + '/api/v1/products', product, { headers:this.headers});
  }

  public editproduct(object: any) {
    return this.http.put(environment.serverUrl + '/api/v1/products', object,{ headers:this.headers});
  }
  public deleteproduct(id: number) {
    return this.http.delete(environment.serverUrl + '/api/v1/products/' + id, { headers:this.headers});
  }
  public getproductbyid(id: string) {
    return this.http.get(environment.serverUrl + '/api/v1/products/'+ id, { headers:this.headers});
  }
  public getallproducts(object: any) {
    return this.http.get(environment.serverUrl + '/api/v1/products',{ headers:this.headers});
  }
  public getallproductsAdmin(object: any) {
    return this.http.get(environment.serverUrl + '/api/v1/products/admin',{ headers:this.headers});
  }

  public getproducthistorybyproductid(object: any) {
    return this.http.post(environment.serverUrl + '/api/Candidate/profilesetting', object);
  }
}
