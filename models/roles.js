module.exports = (sequelize, Sequelize) => {
    const Role = sequelize.define("roles", {
      description: {type: Sequelize.STRING}
    })

    Role.associate = models => {
      //models.roles.hasMany(models.moviePeople, {
        //  foreignKey: 'roleId'
      //})
    }
  
    return Role
  }