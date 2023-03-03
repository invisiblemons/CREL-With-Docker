# build stage
FROM node:14-alpine AS build-admin
WORKDIR /app
COPY package*.json ./
RUN npm i --force
COPY . .
RUN npm run build

# production stage
FROM nginx:alpine
COPY ./nginx.conf /etc/nginx/conf.d/default.conf
COPY --from=build-admin /app/dist /usr/share/nginx/html