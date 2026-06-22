# Image de base configurable
ARG BUILD_FROM=mcr.microsoft.com/mssql/server:2019-latest

FROM ${BUILD_FROM} AS builder

ENV ACCEPT_EULA=Y
ENV SA_PASSWORD=YourStrong!Passw0rd

USER root

# Shim pour que le restore_db.sh fourni (ancien chemin, sans -C) marche avec sqlcmd v18
RUN mkdir -p /opt/mssql-tools/bin && \
    printf '#!/bin/bash\nexec /opt/mssql-tools18/bin/sqlcmd "$@" -C\n' > /opt/mssql-tools/bin/sqlcmd && \
    chmod +x /opt/mssql-tools/bin/sqlcmd

COPY data.sql restore_db.sh /var/opt/mssql/docker/

USER mssql
RUN ( /opt/mssql/bin/sqlservr & ) && bash /var/opt/mssql/docker/restore_db.sh

# On supprime les scripts pour qu'ils ne puissent pas se rendre dans l'image finale
USER root
RUN rm -rf /var/opt/mssql/docker

FROM ${BUILD_FROM}

ENV ACCEPT_EULA=Y
ENV SA_PASSWORD=YourStrong!Passw0rd

# On copie SEULEMENT les donnees cuites depuis le builder (aucun script ne suit)
COPY --from=builder --chown=mssql /var/opt/mssql /var/opt/mssql

COPY entrypoint.sh /usr/src/app/entrypoint.sh

HEALTHCHECK --interval=10s --timeout=5s --retries=10 \
    CMD /opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P "$SA_PASSWORD" -C -Q "SELECT 1" || exit 1

ENTRYPOINT ["/bin/bash", "/usr/src/app/entrypoint.sh"]