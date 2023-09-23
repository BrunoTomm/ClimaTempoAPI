@echo off
:START
docker build --no-cache -f Dockerfile -t clima_local .
docker run --name ClimaLocal --restart unless-stopped  -p 8000:80 clima_local

:: Verificar o status do contêiner
docker ps | find "ClimaLocal"
if %errorlevel% neq 0 (
  echo "O contêiner não está em execução. Reiniciando..."
  docker stop ClimaLocal
  docker rm ClimaLocal
  goto START
) else (
  echo "O contêiner está em execução."
)

:: Aguardar por um tempo antes de verificar novamente (aqui, 5 segundos)
timeout /t 5 >nul
goto START
