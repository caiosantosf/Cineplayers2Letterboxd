module.exports = (sequelize, Sequelize) => {
  const User = sequelize.define("users", {
    name: {type: Sequelize.STRING},
    cpUserId: {type: Sequelize.INTEGER},
    email: {type: Sequelize.STRING},
    picture: {type: Sequelize.STRING}
  });

  return User;
};