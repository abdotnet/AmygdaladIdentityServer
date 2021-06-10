import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { ApiResponse, Pager, ProductResponse } from '../_model/model';
import { ProductService } from '../_service/product.service';

@Component({
  selector: 'app-listproduct',
  templateUrl: './listproduct.component.html',
  styleUrls: ['./listproduct.component.css']
})
export class ListproductComponent implements OnInit {

  page:number = 1;
  pageSize:number = 10;
  totalItems:number =0;
  pageSizes = [20, 25, 50,100];

  private pagem = {
    PageNumber : 1,
    pageSize:50
  }
  data:ProductResponse[] =[];

  constructor(private service : ProductService,  private toastr: ToastrService,
    private router : Router,  private spinner : NgxSpinnerService) { }

  ngOnInit(): void {
    this.getListOfProducts();
  }

  public getListOfProducts()
  {
    this.spinner.show();
    this.service.getallproducts(this.pagem).subscribe(c=>{

      this.spinner.hide()
      console.log(c);
       var response = <ApiResponse<Pager<ProductResponse>>> c;
       if (response.requestSuccessful)
       {
         console.log(response.responseData);
        this.data = response.responseData.result;
         this.page = response.responseData.currentPage;
         this.pageSize = response.responseData.itemsPerPage;
         this.totalItems  = response.responseData.totalItems;

         this.toastr.success("Product data loaded successfully")
       }
       else
       {
         this.toastr.info("Product not found")
       }
    }, error=>{

      this.spinner.hide()
      this.toastr.info("Product failed to load");
    });

  }

  handlePageSizeChange(event: any) {
    //this.spinner.show();
    this.pageSize = event.target.value;
    this.page = 1;
    this.getListOfProducts();
   // this.spinner.hide();
  }

  handlePageChange(event : any) {
   // this.spinner.show();
    this.page = event;
    this.getListOfProducts();
    //this.spinner.hide();
  }
  public RefreshOk()
  {
    window.location.reload()
  }

  public deleteProduct(id : number)
  {
    if (confirm('Are you sure'))
    {
      this.service.deleteproduct(id).subscribe(c=>{

        console.log(c);
        var resp = <ApiResponse<ProductResponse>> c;

        if (resp.requestSuccessful)
        {
          this.toastr.success("Product deleted successfully");
        this.getListOfProducts();
        }
        else{
          this.toastr.error("Product failed to delete");
        }
      }, error=>{
        this.toastr.error("Product failed to be deleted");
      })
    }
  }
  public editProduct(id : number)
  {
    this.router.navigate(['/edit-product', id]);

  }
}
