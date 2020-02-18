import { Component, OnInit, ChangeDetectorRef, Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpInterceptor, HttpHandler, HttpRequest, HttpEvent } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import Axios, { AxiosResponse } from 'axios'

const api = 'https://localhost:44317';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

  title = 'jwtClient';
  tokken: string = null;
  names : string[]=[];
  constructor(private http: HttpClient) {

  }
  ngOnInit(): void {
    
  }

  getTokken(){
    let myHeaders = new HttpHeaders();
    myHeaders = myHeaders.append('content-type', 'application/json');

    this.http.post(`${api}/login`, {Name: 'Kabel'}).subscribe((x: any)=> {
      this.tokken = x.data;
    });
  }

  cleanTokken(){
    this.tokken=null;
  }

  getNames(){
    Axios.defaults.headers.common['Authorization'] = 'Bearer ' + this.tokken;

    Axios.get(`${api}/name`).then(x=>{
      this.names = x.data;
    }).catch(err=>{
      this.names = [JSON.stringify(err)];
      debugger;
    })

    /*debugger;
    const customHeaders = new HttpHeaders({
      Authorization: `Bearer ${this.tokken}`,
      'Content-Type': 'application/json'
    });

    this.http.get(`${api}/name`,{headers:customHeaders}).subscribe((x: any) => {
      this.names = x.data;
      
    }, err => {
      this.names = [JSON.stringify(err)];
      debugger;
    });*/
  }

}
