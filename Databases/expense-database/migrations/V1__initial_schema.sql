create table expense(
 id         bigint not null auto_increment,
 user_id    bigint not null, 
 project_id bigint not null,
 name       VARCHAR(255) not null,
 exp_typ    VARCHAR(50) not null,
 amount     DECIMAL(12,2)   not null,
 date datetime,

 primary key (id)
)
engine = innodb
default charset = UTF8MB4;