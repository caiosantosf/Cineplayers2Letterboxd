module.exports = (sequelize, Sequelize) => {
    const Role = sequelize.define("roles", {
      description: {type: Sequelize.STRING}
    })
  
    return Role
  }