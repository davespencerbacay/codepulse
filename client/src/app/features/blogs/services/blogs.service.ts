import { Injectable } from '@angular/core';
import { AddBlogPost } from '../models/add-blog-model';
import { Observable } from 'rxjs';
import { Blog } from '../models/blog-model';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class BlogsService {

  constructor(private http: HttpClient) { }

  createBlog(data: AddBlogPost): Observable<Blog> {
    return this.http.post<Blog>(`${environment.apiBaseUrl}/api/blogs`, data);
  }

  getAllBlogs(): Observable<Blog[]> {
    return this.http.get<Blog[]>(`${environment.apiBaseUrl}/api/blogs`);
  }

  deleteBlog(id: string): Observable<Blog> {
    return this.http.delete<Blog>(`${environment.apiBaseUrl}/api/blogs/${id}`);
  }
}
