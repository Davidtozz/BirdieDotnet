import faker
import requests

faker = faker.Faker()



for i in range(0, 10):
    
    result = requests.post("http://localhost:5069/api/user/register", json={
        "username" : faker.name(),
        "password" : faker.password(),
        "email" : faker.email()
    })

    print(result.status_code)