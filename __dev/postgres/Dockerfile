FROM postgres:17

# Cài đặt các công cụ cần thiết
RUN apt-get update && apt-get install -y \
    postgresql-server-dev-17 \
    build-essential \
    git \
    gcc \
    make \
    libssl-dev \
    zlib1g-dev \
    pkg-config \
    libprotobuf-c-dev \
    && rm -rf /var/lib/apt/lists/*

# Tải và biên dịch postgres-decoderbufs từ GitHub
RUN git clone https://github.com/debezium/postgres-decoderbufs.git /tmp/postgres-decoderbufs \
    && cd /tmp/postgres-decoderbufs \
    && make \
    && make install \
    && rm -rf /tmp/postgres-decoderbufs

CMD ["postgres"]
