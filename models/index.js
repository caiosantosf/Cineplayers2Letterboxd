const Sequelize = require("sequelize");
var fs = require('fs');
var path = require('path');
var basename = path.basename(__filename);

const sequelize = new Sequelize(process.env.DATABASE_URL, {logging: false});

const db = {};

db.Sequelize = Sequelize;
db.sequelize = sequelize;

if (process.env.ENVIRONMENT == 'development')
  db.sequelize.sync({ force: true })

fs
  .readdirSync(__dirname)
  .filter(file => {
    return (file.indexOf('.') !== 0) && (file !== basename) && (file.slice(-3) === '.js');
  })
  .forEach(file => {
    var model = sequelize['import'](path.join(__dirname, file));
    db[model.name] = model;
  });

  Object.keys(db).forEach(modelName => {
    if (db[modelName].associate) {
      db[modelName].associate(db);
    }
})

module.exports = db;