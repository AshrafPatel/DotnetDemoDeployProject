# 📇 Contacts Management Application  
A full-stack Contacts Management System built with React (Vite + TypeScript) and ASP.NET Core Web API, designed with clean architecture, modern deployment practices, and production-ready configuration.  
   
# Live Demo  
Frontend Blazor (Railway):  
👉 [https://dotnetdemo-ui-production.up.railway.app]  
Backend API (Railway):  
👉 [https://energetic-enthusiasm-production.up.railway.app]  
React Frontend (Vercel)  
👉 [https://dotnet-demo-deploy-project.vercel.app]  
  
## 🧱 Tech Stack  
### Frontend   
⚛️ React 19   
⚡ Vite  
🟦 TypeScript  
🔁 React Router v6  
🌐 Fetch API  
🔐 Environment-based configuration  
  
### Backend   
🟣 ASP.NET Core Web API   
🗂️ Clean Architecture (Core / Infrastructure / Services)   
🔁 AutoMapper    
🧪 DTO-based request/response validation    
🌍 CORS configured for production   
📊 Application Insights logging   
  
### Deployment   
▲ Vercel – Frontend hosting (global CDN)    
🚄 Railway – Backend API hosting   
🔐 Environment variables (no secrets in repo)   
  
## 🏗️ Architecture Overview   
Browser   
   ↓ -- https://react-contacts-ui.vercel.app  
React + Vite (Vercel)     
   ↓ -- HTTPS (REST API)   
ASP.NET Core API (Railway)   
   ↓    
Database (PostGres SQL)   

This separation ensures:  
-Independent scaling
-Faster frontend delivery via CDN
-Clean separation of concerns
   
## ✨ Features
✅ View all contacts  
➕ Create new contacts  
✏️ Edit existing contacts   
🗑️ Delete contacts   
🔄 Enum-backed state selection  
📅 Created date tracking   
🔐 Production-ready CORS handling  
  
## 📂 Project Structure   
### Frontend (React)
src/
 ├─ components/
 │   ├─ ContactRow
 |   ├─ NavBar
 |   └─ NewContactForm
 ├─ pages/
 │   ├─ ContactsPage
 │   └─ NewContactPage
 ├─ enums/
 │   └─ State.ts
 ├─ App.tsx
 └─ main.tsx
  
 ### Frontend (Blazor UI)  
 Services (ContactsApiClient)   
 Component -> Pages (Contacts.razor, NewContact.razor)  
   
### Backend
Contacts.Core   
Contacts.Infrastructure   
Contacts.Services  
Contacts.API  
Contacts.Shared   

## 🔐 Environment Variables
### Frontend (React)
VITE_API_URL=https://energetic-enthusiasm-production.up.railway.app    

### Backend (C# Railway)
Connection strings  
Application Insights  
Environment-specific settings   

## 🧠 Key Engineering Decisions
Enum synchronization between backend and frontend   
DTO-first API design to avoid over-posting   
Environment-based configuration (no hardcoded URLs)   
Separation of frontend and backend deployments   
Type-safe React components   

## 📌 What This Project Demonstrates
Full-stack development skills   
Real-world deployment experience   
Clean architecture principles   
Debugging and configuration of modern tooling (Vite, TS, env vars)   
Understanding of production concerns (CORS, env separation)   

## 👤 Author
Ashraf Patel   
Software Engineer  











