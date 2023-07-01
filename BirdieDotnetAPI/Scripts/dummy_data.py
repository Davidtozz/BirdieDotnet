from faker import Faker
import requests

faker = Faker()



#? Sometimes works, sometimes doesn't idk why

for i in range(0, 10):
    
    data = {
        "username" : faker.name(),
        "password" : faker.password(),
        "email" : faker.email()
    }
    result = requests.Response()


    try:
        result = requests.post("https://127.0.0.1:5069/api/user/register", json=data)
        print(result.status_code)
    except:
        print("Connection refused")
    