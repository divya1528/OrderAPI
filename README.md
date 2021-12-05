# OrderAPI

## This API is used to get order details from database. This is a secure API that uses JWT authentication.

**To request token, use below request**

```
POST
http://localhost:5000/api/v1/login

Body:
{
  "email": "test",
  "password": "test"
}

```

**The credentials will be validated and a token will be generated**

API Response:
```
{
  "token": "token"
}
```


**To get order details, send below request**

```
GET
http://localhost:5000/api/v1
```

API Response:
```
{
  "id": "qer1234fsfsf",
  "userID": "test",
  "products": [

	{
      "productID": "2",
      "productQty": 10,
      "productPrice": 50
    }
  ],
  "status": "processed"
}
```
