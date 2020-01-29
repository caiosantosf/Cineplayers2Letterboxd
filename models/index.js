const Sequelize = require("sequelize");

const sequelize = new Sequelize(process.env.DB_NAME, 
  process.env.DB_USERNAME, 
  process.env.DB_PASSWORD, 
  {
    host: process.env.DB_HOST,
    port: process.env.DB_PORT,
    dialect: 'mysql'
  }
);

const db = {};

db.Sequelize = Sequelize;
db.sequelize = sequelize;

if (process.env.ENVIRONMENT == 'development')
  db.sequelize.sync({ force: true })

db.movies = require("./movies.js")(sequelize, Sequelize);
db.users = require("./users.js")(sequelize, Sequelize);

module.exports = db;