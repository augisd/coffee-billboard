import axios, { AxiosResponse } from "axios";

export interface Coffee {
  id: string;
  title: string;
  price: number;
  imagePath: string;
}

const instance = axios.create({
  baseURL: "https://localhost:5001/api/",
  timeout: 10000,
  // headers: {'X-Custom-Header': 'foobar'}
});

const requests = {
  getCoffeeList: (): Promise<AxiosResponse<Coffee[]>> =>
    instance.get("/coffee"),
  getCoffee: (id: string): Promise<AxiosResponse<Coffee>> =>
    instance.get("/coffee/" + id),
  addNewCoffee: (formData: FormData): Promise<AxiosResponse<Coffee>> =>
    instance.post("/coffee", formData, {
      headers: { "Content-Type": "multipart/form-data;" },
    }),
  deleteCoffee: (id: string): Promise<AxiosResponse> =>
    instance.delete("/coffee/" + id),
};

export default requests;
