import { ApiResponse, ProductResponse } from './../_model/model';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { ProductService } from '../_service/product.service';

@Component({
  selector: 'app-editproduct',
  templateUrl: './editproduct.component.html',
  styleUrls: ['./editproduct.component.css']
})
export class EditproductComponent implements OnInit {

  public model= {
    name:'',
    description:'',
    costPrice:0,
    sellingPrice:0,
    quantity:0,
    id :0
  };
  id : any ;

  constructor(private productService : ProductService,
    private toastr: ToastrService,
    private router : Router,  private spinner : NgxSpinnerService ,private route : ActivatedRoute ) {

     this.id = this.route.snapshot.paramMap.get('id')
     this.getProductById(this.id);
    }

  ngOnInit(): void {
  }

  public getProductById(id : any)
  {
 this.productService.getproductbyid(id).subscribe(c=>{
    var resp= <ApiResponse<ProductResponse>> c;

     if (resp.requestSuccessful)
     {
       this.model.name= resp.responseData.name;
       this.model.description = resp.responseData.description;
       this.model.costPrice= resp.responseData.costPrice;
       this.model.quantity= resp.responseData.quantity;
       this.model.sellingPrice= resp.responseData.sellingPrice;
     }
 });
  }
  public UpdateProduct(model: any)
  {
    console.log(model);
    this.spinner.show();
    model.id = this.id;
    this.productService.editproduct(model).subscribe(c=>{
      this.spinner.hide();
         var resp = <ApiResponse<ProductResponse>> c;

         if (resp.requestSuccessful)
         {
        this.toastr.success("Successful");
         }
         else
         {
          this.toastr.error("failed");
         }

    },error=>{
      this.spinner.hide();
      this.toastr.error("fail to update");
    });

 }

}
