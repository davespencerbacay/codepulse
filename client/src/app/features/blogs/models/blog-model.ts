import { Category } from "../../category/models/category.model";

export interface Blog {
    id: string;
    title: string;
    content: string;
    shortDescription: string;
    featuredImageUrl: string;
    urlHandle: string;
    publishedDate: Date;
    author: string;
    isVisible: boolean; 
    categories: Category[];
} 