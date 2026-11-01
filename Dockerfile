# Шаг 1: официальный Nginx на базе Alpine Linux.
FROM nginx:alpine

# Шаг 2: удаляем стандартную конфигурацию.
RUN rm /etc/nginx/conf.d/default.conf

# Шаг 3: устанавливаем правила раздачи сжатой WebGL-сборки.
COPY nginx.conf /etc/nginx/conf.d/

# Шаг 4: заменяем стандартную страницу файлами игры.
RUN rm -rf /usr/share/nginx/html/*
COPY Builds/WebGL/ /usr/share/nginx/html/

# Шаг 5: рабочий порт контейнера.
EXPOSE 80

# Шаг 6: запускаем Nginx в foreground-режиме.
CMD ["nginx", "-g", "daemon off;"]
