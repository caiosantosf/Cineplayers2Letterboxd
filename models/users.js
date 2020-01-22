module.exports = (sequelize, Sequelize) => {
  const User = sequelize.define("users", {
    name: {
      type: Sequelize.STRING
    },
    cpUser: {
      type: Sequelize.INTEGER
    }
  });

  return User;
};