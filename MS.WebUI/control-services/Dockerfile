# stage as builder
FROM node:16-alpine as builder

# copy the package.json to install dependencies
COPY package.json package-lock.json ./

# Install the dependencies and make the folder
RUN npm cache clean --force && npm install && mkdir /control-services && mv ./node_modules ./control-services

WORKDIR /control-services

COPY . .

# Build the project and copy the files
RUN npm run ng build -- --configuration production --aot

FROM nginx:alpine

#!/bin/sh

COPY ./.nginx/nginx.conf /etc/nginx/nginx.conf

## Remove default nginx index page
RUN rm -rf /usr/share/nginx/html/*

# Copy from the stage
COPY --from=builder /control-services/dist/control-services/ /usr/share/nginx/html

# Add the permission at build time
RUN chown nginx:nginx /usr/share/nginx/html/*

EXPOSE 4200 80

ENTRYPOINT ["nginx", "-g", "daemon off;"]
