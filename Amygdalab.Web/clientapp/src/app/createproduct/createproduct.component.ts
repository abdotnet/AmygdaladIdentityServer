import { ApiResponse, ProductResponse } from './../_model/model';
import { ProductService } from './../_service/product.service';
import { Component, OnInit } from '@angular/core';
import { Product } from '../_model/model';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-createproduct',
  templateUrl: './createproduct.component.html',
  styleUrls: ['./createproduct.component.css']
})
export class CreateproductComponent implements OnInit {

  public model= {
    name:'',
    description:'',
    costPrice:0,
    sellingPrice:0,
    quantity:0
  };

  constructor(private productService : ProductService,
     private toastr: ToastrService,
     private router : Router,  private spinner : NgxSpinnerService) { }

  ngOnInit(): void {

  }

  public SaveProduct(model: any)
  {
    this.spinner.show();
    this.productService.createproduct(model).subscribe(c=>
    {
      this.spinner.hide();
      console.log(c);
       var resp = <ApiResponse<ProductResponse>> c;
      if (resp.requestSuccessful)
        {
         this.toastr.success('Successful');
         model = null;
        }
      else
      {
        this.toastr.error('Failed');
      }

    },error =>{
      this.spinner.hide();
      this.toastr.error('Product failed to save!');
    })
  }

}
