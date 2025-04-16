
# Hack the Vault: Phase One – Identify and Fortify

---

## The Situation  
You’ve joined a dev team midway through a project. The login system is flawed and stores passwords insecurely.  

Attackers are lurking. Your task is to protect the vault (and your users) before it's too late.

---

## Your Mission  
1. **Identify at least 3 password-related security flaws** in the current implementation.  
2. **Refactor the login system** to use secure hashing + salting (C#/.NET).  
3. **Write unit tests** to prove your updated login system works.  
4. **Submit and review at least two peers' code**, giving constructive, security-focused feedback.  

Bonus: Earn extra **Dev Cred** by adding advanced features like 2FA or account lockout.

---

## Learning Outcome  
> At the end of this activity, you’ll be able to design and implement a secure login system in C# using hashing and salting techniques and validate your solution through automated tests.

---

## Repo Structure  

HackTheVault/
├── FlawedLoginSystem/          # Contains the insecure logic you'll be fixing
│   ├── LoginService.cs
│   └── User.cs
├── Tests/                      # Unit tests go here
│   └── LoginTests.cs
├── README.md                   # This guide
---

## Running Your Tests  
Make sure you're in the project root, then:

```bash
cd Tests
dotnet test
```

All tests must pass to complete your mission.

---

## Bonus Challenge Ideas  
Earn extra badges by:

- Adding **2-Factor Authentication**  
- Using **Argon2** instead of PBKDF2  
- Implementing **brute-force lockout**  
- Creating a Razor Pages or Blazor login UI  

