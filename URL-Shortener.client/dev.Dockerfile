FROM node:22-alpine

WORKDIR /app

COPY . .

RUN yarn

RUN yarn global add @angular/cli

CMD ["ng", "serve", "--host", "0.0.0.0"]
